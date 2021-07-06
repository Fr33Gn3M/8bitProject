package com.demo.Controller;

import com.basic.model.JSONResult;
import com.demo.repository.DoctorMapper;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

@RestController
@MapperScan("com.demo.repository")
@RequestMapping("api/doctor")
public class DoctorController {

	@Autowired
	private DoctorMapper doctorMapper;

	@GetMapping("/{id}")
	@ResponseStatus(HttpStatus.OK)
	public JSONResult getDoctorById(@PathVariable("id") int id){
		var x = doctorMapper.selectByPrimaryKey(id);
		return JSONResult.ok(x);
	}
}
