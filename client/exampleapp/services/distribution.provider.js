import angular from 'angular';
import signalR, {HubConnectionBuilder} from '@aspnet/signalr';

angular.module('movetocore.services')
  .factory('Distribution', () => {
    const connection = new HubConnectionBuilder()
      .withUrl('/distribution')
      .build();

    return connection;
  });