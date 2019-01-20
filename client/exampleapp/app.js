import angular from 'angular';
import 'angular-ui-router';
import 'angular-aria';
import 'angular-animate';
import 'angular-messages';
import 'angular-resource';
import 'mdi/css/materialdesignicons.css';
import 'angular-material';
import ngSantinize from 'angular-sanitize';

import providers from './providers';
import services from './services'
import constants from './constants'

import './app.less';

angular.module('movetocore', ['ngMaterial', 'ngMessages', 'ui.router', 'ngResource', ngSantinize, providers, services, constants]);

angular.module('movetocore')
    .config(($urlRouterProvider, $mdThemingProvider) => {
            $urlRouterProvider.otherwise('/');
            ///$locationProvider.html5Mode(true);

            $mdThemingProvider.theme('default')
                .primaryPalette('blue')
                .accentPalette('deep-orange');
        }
    )
  .run((Distribution) => {
    Distribution.start()
      .then(() => console.log('signalR connected'))
  });

require('./home');

