import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Device } from 'src/app/_models/device';
import { DevicesService } from 'src/app/_services/devices.service';

@Component({
  selector: 'app-device-detail',
  templateUrl: './device-detail.component.html',
  styleUrls: ['./device-detail.component.css']
})
export class DeviceDetailComponent implements OnInit {
  device: Device;

  constructor(private devicesService: DevicesService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadDevice();
  }

  loadDevice() {
    this.devicesService.getDevice(parseInt(this.route.snapshot.paramMap.get('id'))).subscribe(device => {
      this.device = device;
    })
  }

}
