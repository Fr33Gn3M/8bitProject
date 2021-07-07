package com.demo.service.impl;

import com.demo.entity.Doctor;
import com.demo.repository.DoctorMapper;
import com.demo.service.DoctorService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class DoctorServiceImpl implements DoctorService {
	@Autowired
	private DoctorMapper doctorMapper;

	@Override
	public Doctor getDoctorById(int id) {
		var doctor = doctorMapper.selectByPrimaryKey(id);
		return doctor;
	}
}
