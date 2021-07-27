package com.demo.controller;

import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;

/**
 * @author freedom
 * @date 2021/07/27
 */
@CrossOrigin
public class BaseController {
    @GetMapping("/run")
    public String isRunning(){
        return "服务运行中!";
    }
}
