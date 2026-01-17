package com.fc.fcbase.basicapi;


import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.scheduling.annotation.EnableAsync;

@EnableAsync  //开启异步]
@SpringBootApplication
public class BasicApiApplication {
    public static void main(String[] args) {
        SpringApplication.run(BasicApiApplication.class, args);
    }

}
