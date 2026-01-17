import { Alova } from '@/utils/http/alova/index';

// api/modules/user.ts

interface ApiResult {
  code: number;
  message: string;
  data: any; // 或更具体的类型
}

/**
 * @description: 获取用户信息 —— 对接 FastAPI /users/me
 */
export function getUserInfo() {
  return Alova.Get<ApiResult>('/users/me', {
    meta: {
      isReturnNativeResponse: true,
    },
  });
}

/**
 * @description: 用户登录 —— 对接 FastAPI /token
 */
export function login(data: { username: string; password: string }) {
  // FastAPI 的 /token 要求 form-data 格式
  const formData = new FormData();
  formData.append('username', data.username);
  formData.append('password', data.password);

  return Alova.Post<ApiResult>('/token', formData, {
    meta: {
      isReturnNativeResponse: true,
      // 关键：设置 Content-Type 为 undefined，让浏览器自动设置 boundary
      headers: {
        'Content-Type': undefined,
      },
    },
  });
}

/**
 * @description: 用户修改密码（可选）
 * 注意：FastAPI 需要你自行实现此接口
 */
export function changePassword(params: any, uid: string) {
  // 示例：假设你有 /users/{uid}/password 接口
  return Alova.Put<ApiResult>(`/users/${uid}/password`, params);
}

/**
 * @description: 用户登出 —— 本地清除 Token
 */
export function logout() {
  // FastAPI 是无状态 JWT，登出只需前端清除 Token
  return Promise.resolve({ data: { success: true } });
}
