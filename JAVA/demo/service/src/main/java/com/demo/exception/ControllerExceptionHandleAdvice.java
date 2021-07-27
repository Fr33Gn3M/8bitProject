package com.demo.exception;

import com.demo.utils.JSONResult;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.ResponseBody;

/**
 * @author freedom
 * @date 2021-07-27
 */
@ControllerAdvice
public class ControllerExceptionHandleAdvice {

	private static final Logger logger = LoggerFactory.getLogger(ControllerExceptionHandleAdvice.class);
	@ResponseBody
	@ExceptionHandler(ServiceException.class)
	public JSONResult serviceExceptionHandle(ServiceException exception){
		logger.error(exception.getMsg(),exception);
		return JSONResult.error(exception.getCode(),exception.getMsg());
	}

	@ResponseBody
	@ExceptionHandler(RuntimeException.class)
	public JSONResult runtimeExceptionHandle(RuntimeException runtimeException){
		logger.error(runtimeException.getMessage(),runtimeException);
		return JSONResult.error(runtimeException.getMessage());
	}
}
