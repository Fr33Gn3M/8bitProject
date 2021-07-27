package com.demo.controller;

import com.demo.dto.DoctorDTO;
import com.demo.service.DoctorService;
import com.demo.utils.JSONResult;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.ApiParam;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

/**
 * @author freedom
 * @date 2021/07/07
 */
@RestController
@MapperScan("com.demo.repository")
@RequestMapping("api/doctor")
@Api(value="博士操作控制器")
public class DoctorController extends BaseController{

	@Autowired
	private DoctorService doctorService;

	@ApiOperation(value = "根据id查询", notes = "根据id查询博士数据")
	@GetMapping("/{id}")
	@ResponseStatus(HttpStatus.OK)
	public JSONResult getDoctorById(@ApiParam(value = "id", required=true)@PathVariable("id") int id){
		var doctor = doctorService.getDoctorById(id);
		DoctorDTO doctorDto = new DoctorDTO();
		BeanUtils.copyProperties(doctor, doctorDto);
		return JSONResult.ok(doctorDto);
	}
}
