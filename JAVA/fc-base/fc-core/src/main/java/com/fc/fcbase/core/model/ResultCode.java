package com.fc.fcbase.core.model;

public enum ResultCode {
    SUCCESS(200, "OK！"),
    FAIL(500, "请求异常！"),
    AUTHFAIL(401, "权限校验失败！");

    private int code;
    private String msg;

    ResultCode(int code, String msg) {
        this.code = code;
        this.msg = msg;
    }

    public int getCode() {
        return code;
    }

    public void setCode(int code) {
        this.code = code;
    }

    public String getMsg() {
        return msg;
    }

    public void setMsg(String msg) {
        this.msg = msg;
    }
}
