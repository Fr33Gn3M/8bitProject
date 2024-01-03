import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import App from './App.vue'

//ElementPlus全局配置对象:size 用于设置表单组件的默认尺寸，zIndex 用于设置弹出组件的层级，zIndex 的默认值为 2000
createApp(App).use(ElementPlus, { size: 'small', zIndex: 3000 }).mount('#app')
