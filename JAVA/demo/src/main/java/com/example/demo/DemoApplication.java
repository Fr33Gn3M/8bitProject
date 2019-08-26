package com.example.demo;

import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@SpringBootApplication
public class DemoApplication {

    @RequestMapping("/")
    public String Run(){
        return "服务运行中!";
    }


//    public static void main(String[] args) {
//        SpringApplication.run(DemoApplication.class, args);
//    }

}
