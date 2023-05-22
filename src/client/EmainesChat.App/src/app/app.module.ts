import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RoomsSideBarComponent } from './Components/rooms-side-bar/rooms-side-bar.component';
import { ChatComponent } from './Components/chat/chat.component';
import { ChatMessagesComponent } from './Components/chat/chat-messages/chat-messages.component';
import { ChatBarComponent } from './Components/chat/chat-bar/chat-bar.component';

@NgModule({
  declarations: [
    AppComponent,
    RoomsSideBarComponent,
    ChatComponent,
    ChatMessagesComponent,
    ChatBarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
