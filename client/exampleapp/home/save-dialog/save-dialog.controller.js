import angular from 'angular';

const LOGIN_CONTROLLER = 'createTestController';

angular.module('movetocore')
  .controller(LOGIN_CONTROLLER, ($scope, Test, $mdDialog) => {
    $scope.save = () => Test.save($scope.test).$promise
        .then(() => $mdDialog.hide());
  });

export default LOGIN_CONTROLLER;