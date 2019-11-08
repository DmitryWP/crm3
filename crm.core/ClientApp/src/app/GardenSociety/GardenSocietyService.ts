import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { retry, map } from 'rxjs/operators';
import { extend } from 'webdriver-js-extender';

// СНТ (информация)
export interface GardenSocietyInfo {
    name: string;
    enabled: boolean;
}

// СНТ
export interface GardenSociety extends GardenSocietyInfo
{
    // Иденитификатор
    id: number;
}


export class GardenSocietyService {

    private requestsUrl = 'api/garden-society';

    constructor(private http: HttpClient) {

    }

    get(): Observable<GardenSociety> {
        return this.http.get<GardenSociety>(`${this.requestsUrl}`, { withCredentials: true });
    }
}
