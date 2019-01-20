import angular from 'angular';
import {findIndex, remove} from 'lodash';

import saveDialog from './save-dialog';

const LOGIN_CONTROLLER = 'homeController';

angular.module('movetocore')
  .controller(LOGIN_CONTROLLER, ($scope, Test, $mdDialog, Distribution, DistributionType) => {
    const loadAllTests = () => Test.get().$promise
      .then(({all}) => {
        $scope.tests = all;
      });

    $scope.openSaveDialog = () => {
      $mdDialog.show(saveDialog)
    };

    $scope.openDeleteDialog = ({id}) => $mdDialog.show($mdDialog.confirm()
      .title('Delete Test')
      .textContent(`Are you sure you want to delete the test with the id ${id}?`)
      .ok('Yes')
      .cancel('Cancel'))
      .then(() => Test.delete({id}).$promise);

    Distribution.on('distribution', ({type, item}) => {
      switch (type) {
        case (DistributionType.Insert): {
          $scope.tests.push(item);

          break;
        }
        case (DistributionType.Update): {
          const updatedTestIndex = findIndex($scope.tests, x => x.id === item.id);

          $scope.tests[updatedTestIndex] = item;

          break;
        }
        case (DistributionType.Delete): {
          $scope.tests = $scope.tests.filter(x => x.id !== item.id);

          break;
        }
      }

      $scope.$apply();
    });

    loadAllTests();
  });

export default LOGIN_CONTROLLER;