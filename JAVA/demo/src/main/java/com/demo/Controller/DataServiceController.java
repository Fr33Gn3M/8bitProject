package com.demo.Controller;

import com.basic.model.JSONResult;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.HashMap;
import java.util.Map;

@RestController
@SpringBootApplication
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

}
