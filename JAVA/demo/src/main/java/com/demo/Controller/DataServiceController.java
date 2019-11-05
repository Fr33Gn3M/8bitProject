package com.demo.Controller;

import com.basic.model.JSONResult;
import com.demo.entity.User;
import com.demo.repository.UserMapper;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.autoconfigure.security.SecurityProperties;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.HashMap;
import java.util.Map;

@RestController
@MapperScan("com.demo.repository")
public class DataServiceController {

    @Autowired
    private UserMapper userMapper;

    @RequestMapping("/DataService")
    public String IsRunning(){
        return "服务运行中!";
    }

    @RequestMapping("/DataService/Query")
    public JSONResult Query(){
        var x = userMapper.selectAll();
        return JSONResult.ok(x);
    }
}
