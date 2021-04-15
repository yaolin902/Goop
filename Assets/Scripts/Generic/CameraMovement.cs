using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D player_rb;
    private Camera camera;

    enum RotateState {
        None,
        //Normal,
        Up,
        Down,
        Left,
        Right
    }
    float max_x_rotate = 9.0f;
    float min_x_rotate = 5.0f;
    //float normal_x_rotate = 7.0f;
    float max_y_rotate = 8.0f;
    float min_y_rotate = 2.0f;
    float rotate_speed = 2.25f;
    float y_offset = 2.0f;
    float smooth_speed = 0.5f;
    float damp_time = 0.3f;
    Vector3 v = Vector3.zero;
    RotateState camera_vertical_state = RotateState.None;
    RotateState camera_horizontal_state = RotateState.None;

    void Start() {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(player.position.x, player.position.y + y_offset, transform.position.z);
        // smoothier camera movement
        Vector3 p = camera.WorldToViewportPoint(player.position);
        Vector3 change = player.position - camera.ViewportToWorldPoint(new Vector3(smooth_speed, smooth_speed, p.z));
        Vector3 d = transform.position + change;
        d.y += y_offset;
        transform.position = Vector3.SmoothDamp(transform.position, d, ref v, damp_time);

        // rotate camera
        // vertical rotation
        if (player_rb.velocity.y > 0 || camera_vertical_state == RotateState.Up) {
            if (this.transform.rotation.eulerAngles.x > max_x_rotate)
                camera_vertical_state = RotateState.None;
            else {
                camera_vertical_state = RotateState.Up;
                this.transform.Rotate(rotate_speed * Time.deltaTime, 0.0f, 0.0f);
            }
        } else if (player_rb.velocity.y < 0 || camera_vertical_state == RotateState.Down) {
            if (this.transform.rotation.eulerAngles.x < min_x_rotate)
                camera_vertical_state = RotateState.None;
            else {
                camera_vertical_state = RotateState.Down;
                this.transform.Rotate(-rotate_speed * Time.deltaTime, 0.0f, 0.0f);
            }
        }

        // horizontal rotation
        if (player_rb.velocity.x > 0 || camera_horizontal_state == RotateState.Right) {
            camera_horizontal_state = RotateState.Right;
            if (this.transform.rotation.eulerAngles.y > max_y_rotate)
                camera_horizontal_state = RotateState.None;
            else
                this.transform.Rotate(0.0f, rotate_speed * Time.deltaTime, 0.0f);
        } else if (player_rb.velocity.x < 0 || camera_horizontal_state == RotateState.Left) {
            camera_horizontal_state = RotateState.Left;
            if (this.transform.rotation.eulerAngles.y < min_y_rotate)
                camera_horizontal_state = RotateState.None;
            else
                this.transform.Rotate(0.0f, -rotate_speed * Time.deltaTime, 0.0f);
        }
    }
}
