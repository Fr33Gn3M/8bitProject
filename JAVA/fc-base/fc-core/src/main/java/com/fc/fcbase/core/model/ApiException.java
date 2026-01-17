package com.fc.fcbase.core.model;

import java.io.Serial;

public class ApiException extends RuntimeException  {
    @Serial
    private static final long serialVersionUID = 8247610319171014183L;
    private final String msg;
    private ResultCode code = ResultCode.FAIL;

    public String getMsg() {
        return msg;
    }

    public ResultCode getCode() {
        return code;
    }

    public ApiException(String message) {
        super(message);
        this.msg = message;
    }

    public ApiException(ResultCode exceptionEnum) {
        super(exceptionEnum.getMsg());
        this.code = exceptionEnum;
        this.msg = exceptionEnum.getMsg();
    }
}
