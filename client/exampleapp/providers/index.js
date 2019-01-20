import angular from "angular";
import 'angular-resource';

const MODULE = 'movetocore.providers';

angular.module(MODULE, ['ngResource']);

require('./test.provider');

export default MODULE;