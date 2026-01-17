package com.fc.fcbase.business.controller;

import com.fc.fcbase.core.model.Result;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.Parameters;
import io.swagger.v3.oas.annotations.enums.ParameterIn;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.extern.slf4j.Slf4j;
import org.springframework.web.bind.annotation.*;

@Slf4j
@RestController
@RequestMapping("/api/business")
@Tag(name = "business业务接口")
public class BusinessController {
    @Operation(summary = "test测试接口")
    @Parameters({
            @Parameter(name = "id",description = "数据id",in = ParameterIn.PATH)
    })
    @GetMapping("/test/{id}")
    public Result<Object> test(@PathVariable("id") String id) {
        return Result.ok("yes");
    }
}
