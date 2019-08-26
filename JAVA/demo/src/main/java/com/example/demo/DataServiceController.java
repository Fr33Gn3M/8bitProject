package com.example.demo;

import com.basic.model.JSONResult;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.HashMap;
import java.util.Map;

@RestController
@SpringBootApplication
@MapperScan("com.repository")
public class DataServiceController {

    @RequestMapping("/DataService")
    public String IsRunning(){
        return "服务运行中!";
    }

    @RequestMapping("/DataService/Query")
    public JSONResult Query(){
        Map map1 = new HashMap<String,Object>();

        return JSONResult.ok(map1);
    }

    public static void main(String[] args) {
        SpringApplication.run(DataServiceController.class, args);
    }
}
