import { AuthenticationService } from '@core/authentication/authentication.service';
import { EventEmitter, Injectable, OnDestroy, OnInit, Output } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Injectable({
	providedIn: 'root'
})
export class BaseSignalrService<T> implements OnInit, OnDestroy {
	private hubConnection: HubConnection;
	private readonly hubURl: string;
	private readonly methodName: string;
	private token: string;

	@Output() signalReceived = new EventEmitter<T>();

	constructor(private authService: AuthenticationService, hubUrl: string, methodName: string) {
		this.hubURl = hubUrl;
		this.methodName = methodName;
		this.ngOnInit();
	}

	ngOnInit(): void {
		this.token = this.authService.getToken();
		if (this.token) {
			this.buildConection();
			this.startConnection();
		}
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

	ngOnDestroy(): void {
		this.hubConnection.stop().then(() => {
			console.log('Connection stopped');
		});
	}
}
