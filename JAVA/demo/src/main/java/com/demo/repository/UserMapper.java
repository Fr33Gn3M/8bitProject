package com.demo.repository;

import com.demo.entity.User;
import org.springframework.stereotype.Component;

import java.util.List;

@Component(value = "UserMapper")
public interface UserMapper {

    /**
     * 新增用户
     * @param user
     * @return
     */
    int save (User user);

    /**
     * 更新用户信息
     * @param user
     * @return
     */
    int update (User user);

    /**
     * 根据id删除
     * @param id
     * @return
     */
    boolean deleteById (String id);

    /**
     * 根据id查询
     * @param id
     * @return
     */
    User selectById (String id);

    /**
     * 查询所有用户信息
     * @return
     */
    List<User> selectAll ();
}
