package com.demo.exception;

import lombok.Getter;

/**
 * @author freedom
 * @date 2021-07-27
 */
@Getter
public enum ExceptionEnum {
	//token认证错误
	TOKEN_IS_INVALID(5002,"token认证错误");
	private final int code;
	private final String msg;
	ExceptionEnum(int code,String msg){
		this.code = code;
		this.msg = msg;
	}
}
