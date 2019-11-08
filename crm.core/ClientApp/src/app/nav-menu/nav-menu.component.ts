import { Component, OnInit } from '@angular/core';

import { MenuItem } from 'primeng/api';

@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css']
})

export class NavMenuComponent implements OnInit {

    isExpanded = false;
    private items: MenuItem[];

//         <li class="nav-item"[routerLinkActive] = "['link-active']" >
//    <a class="nav-link text-dark"[routerLink] = "['/garden-society']" > СНТ < /a>

    ngOnInit(): void {
        this.items = [{
            label: 'File',
            items: [
                { label: 'New', icon: 'pi pi-plus', routerLink: "garden-society" },
                { label: 'Open', icon: 'pi pi-download' }
            ]
        },
        {
            label: 'Edit',
            items: [
                { label: 'Undo', icon: 'pi pi-refresh' },
                { label: 'Redo', icon: 'pi pi-repeat' }
            ]
        }];
    }
}

