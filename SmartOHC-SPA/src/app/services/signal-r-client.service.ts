import { Injectable, EventEmitter } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { SignalViewModel } from '../models/signal-view-model';

@Injectable({
  providedIn: 'root'
})
export class SignalRClientService {

  private hubConnection: signalR.HubConnection;
  signalReceived = new EventEmitter<SignalViewModel>();

  constructor() {
    this.buildConnection();
    this.startConnection();
  }


  private buildConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:54849/signalHub") // .withUrl('http://localhost:54849/signalHub')
      .build();
  };

  public startConnection = () => {
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
        this.registerSignalEvents();
      })
      .catch(err => {
        console.log('Error has occured' + err);
        // start connection again in case of error
        setTimeout(function(){
          this.startConnection();
        }, 3000);
      });
  };

  private registerSignalEvents(){
   
    this.hubConnection.on('SignalMessageReceived', (data: SignalViewModel) => {
      console.log(data);
      this.signalReceived.emit(data);
    });
  }
}
