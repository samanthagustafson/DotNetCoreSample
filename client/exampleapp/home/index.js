import angular from 'angular';

import './home.less';
import controller from './home.controller';

import template from './home.html';

angular.module('movetocore')
    .config($stateProvider => {
        $stateProvider.state("login", {
            url: '/',
            template,
            controller
        });
    });