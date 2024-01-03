package com.fc.fcbase.core.model;

import com.fasterxml.jackson.annotation.JsonProperty;

public class Result<T> {
    @JsonProperty("status")
    private int code;
    /**
     * 响应描述信息
     */
    @JsonProperty("msg")
    private String msg;
    /**
     * 响应数据
     */
    @JsonProperty("data")
    private T data;
    /**
     * 数据总数
     */
    @JsonProperty("total")
    private int total;


    // region get,set方法
    public void setCode(ResultCode resultCode) {
        this.code = resultCode.getCode();
        this.msg = resultCode.getMsg();
    }


    public String getMsg() {
        return msg;
    }
    public void setMsg(String msg) {
        this.msg = msg;
    }

    public void setData(T data) {
        this.data = data;
    }

    public void setTotal(int total) {
        this.total = total;
    }
    // endregion

    public Result(ResultCode resultCode) {
        this.setCode(resultCode);
    }

    public Result(T data) {
        this(ResultCode.SUCCESS);
        this.data = data;
    }

    public Result(ResultCode resultCode, String msg) {
        this.setCode(resultCode);
        this.setMsg(msg);
    }

    public static <T>Result<T> ok() {
        return new Result<>(ResultCode.SUCCESS);
    }

    public static <T>Result<T> ok(T data) {
        return new Result<>(data);
    }

    public static <T> Result<T> error(ResultCode code) {
        return new Result<>(code);
    }

    public static <T> Result<T> error(String msg) {
        return Result.error(ResultCode.FAIL, msg);
    }

    public static <T> Result<T> error(ResultCode code, String msg) {
        return new Result<>(code, msg);
    }

}
