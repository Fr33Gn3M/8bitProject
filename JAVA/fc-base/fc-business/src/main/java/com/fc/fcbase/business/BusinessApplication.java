package com.fc.fcbase.business;

import com.fc.fcbase.basicapi.BasicApiApplication;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Import;
import org.springframework.scheduling.annotation.EnableAsync;

@EnableAsync  //开启异步]
@SpringBootApplication
@Import({BasicApiApplication.class})
public class BusinessApplication {
    public static void main(String[] args) {
        SpringApplication.run(BusinessApplication.class, args);
    }

}
