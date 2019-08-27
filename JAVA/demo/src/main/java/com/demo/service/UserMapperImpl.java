package com.demo.service;

import com.demo.entity.User;
import com.demo.repository.UserMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class UserMapperImpl implements UserMapper {

    @Autowired
    private UserMapper userMapper;

    @Override
    public int save(User user) {
        return userMapper.save(user);
    }

    @Override
    public int update(User user) {
        return 0;
    }

    @Override
    public boolean deleteById(String id) {
        return false;
    }

    @Override
    public User selectById(String id) {
        return null;
    }

    @Override
    public List<User> selectAll() {
        return null;
    }
}
