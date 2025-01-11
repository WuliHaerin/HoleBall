using LightDev.Pool;

namespace HoleBall
{
  public static class ObstacleFactory
  {
    public static Obstacle GetObstacle(ObstacleType obstacleType)
    {
      switch (obstacleType)
      {
        case ObstacleType.Cube:
          return PoolsManager.RetrieveElement<ObstacleCube>();
        case ObstacleType.Cylinder:
          return PoolsManager.RetrieveElement<ObstacleCylinder>();
        case ObstacleType.Crystal:
          return PoolsManager.RetrieveElement<ObstacleCrystal>();
        case ObstacleType.Prism:
          return PoolsManager.RetrieveElement<ObstaclePrism>();
        default:
          throw new System.NotSupportedException();
      }
    }
  }
}
