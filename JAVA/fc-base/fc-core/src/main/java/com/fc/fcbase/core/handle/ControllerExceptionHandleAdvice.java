package com.fc.fcbase.core.handle;

import com.fc.fcbase.core.model.ApiException;
import com.fc.fcbase.core.model.Result;
import lombok.extern.slf4j.Slf4j;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.ResponseBody;

/**
 * @author fangzp
 */
@Slf4j
@ControllerAdvice
public class ControllerExceptionHandleAdvice {

	@ResponseBody
	@ExceptionHandler(ApiException.class)
	public Result<Object> serviceExceptionHandle(ApiException exception){
		log.error(exception.getMsg(), exception);
		return Result.error(exception.getCode(), exception.getMsg());
	}

	@ResponseBody
	@ExceptionHandler(RuntimeException.class)
	public Result<Object> runtimeExceptionHandle(RuntimeException runtimeException){
		log.error(runtimeException.getMessage(),runtimeException);
		return Result.error(runtimeException.getMessage());
	}

	@ResponseBody
	@ExceptionHandler(Exception.class)
	public Result<Object> runtimeExceptionHandle(Exception exception){
		log.error(exception.getMessage(),exception);
		return Result.error(exception.getMessage());
	}
}
