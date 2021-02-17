import { PrivateModule } from '../../core/shared/data-models/private-module';

export interface SidebarConfigItem {
  feature: string;
  displayName: string;
  sub?: SidebarConfig;
}

export type SidebarConfig = SidebarConfigItem[];

export interface NavConfigItem {
  module: PrivateModule;
  displayName: string;
  sub?: SidebarConfig;
}

export type NavConfig = NavConfigItem[];
