package com.demo.utils;

import lombok.Data;

/**
 * @author freedom
 * @date 2021-07-27
 */
@Data
public class JSONResult {

    /**
     * 响应业务状态
     */
    private int status;

    /**
     * 响应消息
     */
    private String msg;

    /**
     * 响应数据
     */
    private Object data;

    public static JSONResult build(int status, String msg, Object data) {
        return new JSONResult(status, msg, data);
    }

    public static JSONResult ok() {
        return new JSONResult(null);
    }

    public static JSONResult ok(Object data) {
        return new JSONResult(data);
    }

    public static JSONResult error(String msg) {
        return new JSONResult(500, msg, null);
    }

    public static JSONResult error(int code, String msg) {
        return new JSONResult(code, msg, null);
    }

    public JSONResult() {

    }

    public JSONResult(int status, String msg, Object data) {
        this.status = status;
        this.msg = msg;
        this.data = data;
    }

    public JSONResult(Object data) {
        this.status = 200;
        this.msg = "OK";
        this.data = data;
    }

    public Boolean isOK() {
        return this.status == 200;
    }
}
