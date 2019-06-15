import Vue from 'vue'
import App from './App.vue'
// import 'bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css';

import VueNotifications from 'vue-notifications'
import miniToastr from 'mini-toastr'


window.$ = window.jQuery = require('jquery');

Vue.config.productionTip = false;

new Vue({
  render: h => h(App)
}).$mount('#app');


miniToastr.init()

function toast ({title, message, type, timeout, cb}) {
    return miniToastr[type](message, title, timeout, cb)
}

const options = {
    success: toast,
    error: toast,
    info: toast,
    warn: toast
}

Vue.use(VueNotifications, options)