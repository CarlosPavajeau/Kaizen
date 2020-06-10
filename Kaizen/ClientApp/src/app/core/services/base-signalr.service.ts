import { Injectable, Output, EventEmitter, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Injectable({
	providedIn: 'root'
})
export class BaseSignalrService<T> implements OnInit {
	private hubConnection: HubConnection;
	private readonly hubURl: string;
	private readonly methodName: string;
	protected token: string;

	@Output() signalReceived = new EventEmitter<T>();

	constructor(hubUrl: string, methodName: string) {
		this.hubURl = hubUrl;
		this.methodName = methodName;
	}

	ngOnInit(): void {
		this.buildConection();
		this.startConnection();
	}

	private buildConection(): void {
		this.hubConnection = new HubConnectionBuilder()
			.withUrl(this.hubURl, { accessTokenFactory: () => this.token })
			.build();
	}

	private startConnection(): void {
		this.hubConnection
			.start()
			.then(() => {
				console.log('Conenction started');
				this.registerSignalEvents();
			})
			.catch((err) => {
				console.log('Error: ' + err);
				setTimeout(() => this.startConnection(), 5000);
			});
	}

	private registerSignalEvents(): void {
		this.hubConnection.on(this.methodName, (data: T) => {
			this.signalReceived.emit(data);
		});
	}
}
