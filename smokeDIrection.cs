using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class smokeDirection : MonoBehaviour
{
    [Header("Cone Spread Settings")]
    public float speed = 2f;     
    public float spreadAngle = 35f;  

    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void SetDirection(bool facingRight)
    {
        float angle = facingRight ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        var vel = ps.velocityOverLifetime;
        vel.enabled = true;
        vel.space = ParticleSystemSimulationSpace.World;

        float direction = facingRight ? 1f : -1f;

       
        vel.x = new ParticleSystem.MinMaxCurve(
            speed * direction * 0.8f,
            speed * direction
        );

        //spread
        vel.y = new ParticleSystem.MinMaxCurve(-1.5f, 1.5f);
        vel.z = new ParticleSystem.MinMaxCurve(0f, 0f);

        //shape to cone
        var shape = ps.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = spreadAngle;
        shape.radius = 0.1f;
    }
}