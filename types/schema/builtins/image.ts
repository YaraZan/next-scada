import type { NodeProperties, NodeProperty } from '../properties';
import type { Node } from '../node';

type ImagePropertyKeys = 'backgroundImage' | 'objectFit';
type ImageProperties = Record<ImagePropertyKeys, NodeProperty> & NodeProperties;

export interface Image extends Node {
  properties?: ImageProperties;
}
