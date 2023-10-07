import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RoomsSideBarComponent } from './Components/rooms-side-bar/rooms-side-bar.component';
import { ChatComponent } from './Components/chat/chat.component';
import { ChatMessagesComponent } from './Components/chat/chat-messages/chat-messages.component';
import { ChatBarComponent } from './Components/chat/chat-bar/chat-bar.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './Components/home/home.component';
import { LoginModule } from './Components/login/login.module';

@NgModule({
    declarations: [
        AppComponent,
        RoomsSideBarComponent,
        ChatComponent,
        ChatMessagesComponent,
        ChatBarComponent,
        HomeComponent,
    ],
    imports: [
        ReactiveFormsModule,
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        RouterModule,
        LoginModule,
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
