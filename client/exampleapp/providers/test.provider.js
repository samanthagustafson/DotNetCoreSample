import angular from 'angular';

angular.module('movetocore.providers')
  .factory('Test', ($resource) => $resource('/api/test/:id', {
    id: '@id'
  }));