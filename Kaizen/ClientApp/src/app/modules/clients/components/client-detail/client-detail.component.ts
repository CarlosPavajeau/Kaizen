import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Client } from '@modules/clients/models/client';
import { ClientService } from '@modules/clients/services/client.service';

@Component({
  selector: 'app-client-detail',
  templateUrl: './client-detail.component.html',
  styleUrls: [ './client-detail.component.scss' ]
})
export class ClientDetailComponent implements OnInit {
  client: Client;

  constructor(private clientService: ClientService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    this.clientService.getClient(id).subscribe((client) => {
      this.client = client;
    });
  }
}
