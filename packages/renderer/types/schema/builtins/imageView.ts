import type { Node, NodeProperty, NodeProperties } from '../node';

type ImagePropertyKeys = 'backgroundImage';
type ImageProperties = Record<ImagePropertyKeys, NodeProperty> & NodeProperties;

export interface ImageView extends Node {
  properties?: ImageProperties
}
