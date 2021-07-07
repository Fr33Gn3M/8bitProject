package com.demo.controller;

import org.mybatis.spring.annotation.MapperScan;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@MapperScan("com.demo.repository")
public class DataServiceController {

    @GetMapping("/DataService")
    public String IsRunning(){
        return "服务运行中!";
    }

}
