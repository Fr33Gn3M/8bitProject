import { defineStore } from 'pinia';
import { store } from '@/store';
import { ACCESS_TOKEN, CURRENT_USER, IS_SCREENLOCKED } from '@/store/mutation-types';
import { ResultEnum } from '@/enums/httpEnum';

import { getUserInfo as getUserInfoApi, login } from '@/api/system/user';
import { storage } from '@/utils/Storage';

export type UserInfoType = {
  // TODO: add your own data
  username: string;
  email: string;
};

export interface IUserState {
  token: string;
  username: string;
  welcome: string;
  avatar: string;
  permissions: any[];
  info: UserInfoType;
}

export const useUserStore = defineStore({
  id: 'app-user',
  state: (): IUserState => ({
    token: storage.get(ACCESS_TOKEN, ''),
    username: '',
    welcome: '',
    avatar: '',
    permissions: [],
    info: storage.get(CURRENT_USER, {}),
  }),
  getters: {
    getToken(): string {
      return this.token;
    },
    getAvatar(): string {
      return this.avatar;
    },
    getNickname(): string {
      return this.username;
    },
    getPermissions(): [any][] {
      return this.permissions;
    },
    getUserInfo(): UserInfoType {
      return this.info;
    },
  },
  actions: {
    setToken(token: string) {
      this.token = token;
    },
    setAvatar(avatar: string) {
      this.avatar = avatar;
    },
    setPermissions(permissions) {
      this.permissions = permissions;
    },
    setUserInfo(info: UserInfoType) {
      this.info = info;
    },
    // 登录
    async login(params: any) {
      const response = await login(params);
      const { data, code } = response;
      if (code === ResultEnum.SUCCESS) {
        const ex = 7 * 24 * 60 * 60;
        storage.set(ACCESS_TOKEN, data.access_token, ex);
        this.setToken(data.access_token);

        try {
          this.getInfo(ex);
        } catch (error) {
          // 清除 token（避免无效状态）
          storage.remove(ACCESS_TOKEN);
          this.setToken('');
          throw error;
        }

        storage.set(IS_SCREENLOCKED, false);
      }
      return response;
    },

    // 获取用户信息
    async getInfo(param: number) {
      const userInfoResponse = await getUserInfoApi(); // 调用 /api/users/me
      const { code: infoCode, data: userInfo } = userInfoResponse;

      if (infoCode !== ResultEnum.SUCCESS) {
        throw new Error('获取用户信息失败');
      }
      // 4. 保存完整用户信息
      storage.set(CURRENT_USER, userInfo, param);
      this.setUserInfo(userInfo);
      //TODO 角色菜单应该是在这里设置
      //this.setPermissions(userInfo.permissions || []);
      this.setAvatar(userInfo.avatar || '');
      return userInfo;
    },

    // 登出
    async logout() {
      this.setPermissions([]);
      this.setUserInfo({ username: '', email: '' });
      storage.remove(ACCESS_TOKEN);
      storage.remove(CURRENT_USER);
    },
  },
});

// Need to be used outside the setup
export function useUser() {
  return useUserStore(store);
}
