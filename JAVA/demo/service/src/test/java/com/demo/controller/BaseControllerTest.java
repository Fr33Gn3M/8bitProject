package com.demo.controller;

import com.demo.BaseApplicationTest;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;


public class BaseControllerTest extends BaseApplicationTest {

    @Autowired
    private BaseController baseController;

    @Test
    public void isRunning() {
    }

}