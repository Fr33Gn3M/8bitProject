package com.demo.Controller;

import org.mybatis.spring.annotation.MapperScan;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@MapperScan("com.demo.repository")
@RequestMapping("/doctor")
public class DoctorController {

}
