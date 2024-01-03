package com.fc.fcbase.business.config;

import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Info;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class OpenApiConfig {
    @Bean
    public OpenAPI springShopOpenAPI() {
        return new OpenAPI()
                .info(
                        // 接口文档标题
                        new Info().title("Knife4j OpenApi 3")
                        // 接口文档描述
                        .description("Knife4j OpenApi 3 example application")
                        // 接口文档版本
                        .version("v1.0")
                        // 额外补充说明
                        .description("Github example code")
                        // 额外补充链接
                        );
    }
}
