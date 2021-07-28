package com.demo.exception;

/**
 * @author freedom
 * @date 2021-07-27
 */
public class ServiceException extends RuntimeException {
	private String msg;
	private int code = 500;

	public ServiceException(String msg) {
		super(msg);
		this.msg = msg;
	}
	public ServiceException(ExceptionEnum exceptionEnum) {
		super(exceptionEnum.getMsg());
		this.msg = exceptionEnum.getMsg();
		this.code = exceptionEnum.getCode();

	}
	public String getMsg() {
		return msg;
	}
	public int getCode() {
		return code;
	}
}
