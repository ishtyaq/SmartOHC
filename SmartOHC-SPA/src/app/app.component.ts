import { Component, OnInit } from '@angular/core';
import { SignalRClientService } from './services/signal-r-client.service';
import { SignalViewModel } from './models/signal-view-model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(private signalRService: SignalRClientService){

  }
  signalList: SignalViewModel[] = [];

  title = 'SmartOHC-SPA';
  ngOnInit() {
    this.signalRService.signalReceived.subscribe( (signalData: SignalViewModel) => {
      this.signalList.push(signalData);
    });
  }


}
