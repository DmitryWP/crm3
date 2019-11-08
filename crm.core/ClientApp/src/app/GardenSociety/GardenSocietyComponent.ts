import { Component, OnInit } from '@angular/core';
import { GardenSocietyService, GardenSociety, GardenSocietyInfo } from "./GardenSocietyService"
import { Router } from '@angular/router';

export interface Error {
    error: string;
    message: string;
    name: string;
    ok: boolean;
    status: number;
    statusText: string;
    url: string;
}

@Component({
    selector: 'app-garden-society',
    templateUrl: './garden-society.component.html',
    providers: [
        GardenSocietyService
    ]
})



export class GardenSocietyComponent implements OnInit {

    private gs: GardenSociety;

    constructor(private service: GardenSocietyService, private router: Router) {

    }

    ngOnInit(): void {
        this.service
            .get()
            .toPromise()
            .then((res: GardenSociety) => {
                this.gs = res;
            })
            .catch((reason: Error) => {
                alert(reason.ok + ' : ' + reason.message);
            })
    }
}
