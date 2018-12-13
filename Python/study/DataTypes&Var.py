# 十六进制
print(0xd34)
# 浮点数
print(1.23e5)
# 转义字符
print('I\'m \"ok\"')
# r''忽略转移字符
print(r'I\'m ok')
print(r'she is "Batt"')
# '''多行
print('''yes,we
can do it
''')
# r和'''
print(r'''hello \n
world
''')
# 布尔值
print(True and False)
print(True or False)
print(not True)
# 变量
a = 123
print(a)
a = 'abc'
print(a)
# 值传递
b = a
print(b)
a = 'efg'
print(b)
# 常量，不同的是，可以重新给常量赋值，全部大写作为常量只是习惯用法
PI = 3.1415926
# 整数的除法在python中永远是准确的
# 普通除法,结果为浮点数
print(10/3)
print(9/3)
# 地板除，结果为整数
print(10//3)
# 取余
print(10 % 3)
