package com.fc.fcbase.basicapi.controller;

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
@RequestMapping("/api/data")
@Tag(name = "data基础数据接口")
public class DataController {

    @Operation(summary = "id取数")
    @Parameters({
            @Parameter(name = "tb",description = "数据表名",in = ParameterIn.PATH),
            @Parameter(name = "id",description = "数据id",in = ParameterIn.PATH)
    })
    @GetMapping("/info/{tb}/{id}")
    public Result<Object> info(@PathVariable("tb") String tb, @PathVariable("id") String id) {
        return Result.ok("yes");
    }
}
