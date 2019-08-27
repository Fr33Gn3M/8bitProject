package com.demo.Controller;

import com.basic.model.JSONResult;
import com.demo.entity.User;
import com.demo.service.UserMapperImpl;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
@SpringBootApplication
public class DataServiceController {
    @Autowired
    private UserMapperImpl userMapperImpl;


    @RequestMapping("/DataService")
    public String IsRunning(){
        return "服务运行中!";
    }

    @RequestMapping("/DataService/Query")
    public JSONResult Query(){
        Map map1 = new HashMap<String,Object>();

        return JSONResult.ok(map1);
    }

    @PostMapping
    public JSONResult save(@RequestBody User user) {
        var usre = userMapperImpl.save(user);
        ArrayList<User> userList = new ArrayList<User>();
        userList.add(user);
        return JSONResult.ok(userList);
    }

}
