package com.demo.Controller;

import com.demo.BaseApplicationTest;
import org.junit.Assert;
import org.junit.Test;
import org.springframework.beans.factory.annotation.Autowired;


public class DataServiceControllerTest extends BaseApplicationTest {

    @Autowired
    private DataServiceController dataServiceController;

    @Test
    public void isRunning() {
        Assert.assertSame("错误消息1？","服务运行中!",dataServiceController.IsRunning());
    }

    @Test
    public void query() {
        Assert.assertSame("错误消息2？","所有用户",dataServiceController.Query());
    }
}