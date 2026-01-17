# main.py
from fastapi import FastAPI, Depends, HTTPException, status
from fastapi.middleware.cors import CORSMiddleware
from fastapi.security import OAuth2PasswordRequestForm
from fastapi.security import OAuth2PasswordBearer
from jose import jwt
from passlib.context import CryptContext
from datetime import datetime, timedelta
from typing import Optional

from common.response import ResponseModel
from database import init_db, get_user_by_username, create_user
from common.models import UserCreate, Token, UserInfo

# 初始化数据库
init_db()

# 安全配置
SECRET_KEY = "your-secret-key-change-in-production"  # 生产环境务必更换！
ALGORITHM = "HS256"
ACCESS_TOKEN_EXPIRE_MINUTES = 30

pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")

app = FastAPI(title="FastAPI + SQLite 用户认证示例")

# 允许前端跨域（开发用）
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

# 工具函数
def verify_password(plain_password, hashed_password):
    return pwd_context.verify(plain_password, hashed_password)

def get_password_hash(password: str) -> str:
    print(f"🔍 密码类型: {type(password)}, 值: {repr(password)}")
    print(f"   UTF-8 字节长度: {len(password.encode('utf-8'))}")
    return pwd_context.hash(password)

def create_access_token(data: dict, expires_delta: Optional[timedelta] = None):
    to_encode = data.copy()
    expire = datetime.utcnow() + (expires_delta or timedelta(minutes=15))
    to_encode.update({"exp": expire})
    return jwt.encode(to_encode, SECRET_KEY, algorithm=ALGORITHM)

def authenticate_user(username: str, password: str):
    user = get_user_by_username(username)
    if not user or not verify_password(password, user["hashed_password"]):
        return False
    return user

# 路由
@app.post("/api/register", summary="用户注册")
def register(user: UserCreate):
    hashed_pw = get_password_hash(user.password)
    if not create_user(user.username, hashed_pw, user.email):
        raise HTTPException(
            status_code=status.HTTP_400_BAD_REQUEST,
            detail="用户名已存在"
        )
    return ResponseModel(
        code=200,
        message="用户注册成功",
        data="SUCCESS"
    )

@app.post("/api/token", response_model=ResponseModel, summary="用户登录（获取 token）")
def login(form_data: OAuth2PasswordRequestForm = Depends()):
    user = authenticate_user(form_data.username, form_data.password)
    if not user:
        raise HTTPException(
            status_code=status.HTTP_401_UNAUTHORIZED,
            detail="用户名或密码错误",
            headers={"WWW-Authenticate": "Bearer"},
        )
    access_token_expires = timedelta(minutes=ACCESS_TOKEN_EXPIRE_MINUTES)
    access_token = create_access_token(
        data={"sub": user["username"]}, expires_delta=access_token_expires
    )
    return ResponseModel(
        code=200,
        message="登录成功",
        data=Token(
            access_token = access_token,
            token_type = "bearer"
        )
    )

# 声明 OAuth2 Bearer 方案（用于保护 API）
oauth2_scheme = OAuth2PasswordBearer(tokenUrl="/token")

def get_current_user(token: str = Depends(oauth2_scheme)):
    """从 Bearer Token 中解析并验证用户"""
    from jose import JWTError, jwt
    credentials_exception = HTTPException(
        status_code=status.HTTP_401_UNAUTHORIZED,
        detail="无法验证凭据",
        headers={"WWW-Authenticate": "Bearer"},
    )
    try:
        payload = jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])
        username: str = payload.get("sub")
        if username is None:
            raise credentials_exception
    except JWTError:
        raise credentials_exception
    
    user = get_user_by_username(username)
    if user is None:
        raise credentials_exception
    return user

# 正确的受保护路由
@app.get("/api/users/me", summary="获取当前用户信息（需登录）")
def read_users_me(current_user: dict = Depends(get_current_user)):
    return ResponseModel(
        code=200,
        message="登录成功",
        data=UserInfo(
            username = current_user["username"],
            email = current_user["email"]
        )
    )