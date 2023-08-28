import 'package:json_annotation/json_annotation.dart';

part 'film.g.dart';

@JsonSerializable()
class Film {
  final int id;
  final String bango;
  final String title;
  final DateTime? published_at;
  final List<String>? cover_images;

  Film(
      {required this.id,
      required this.bango,
      required this.title,
      this.published_at,
      this.cover_images});

  factory Film.fromJson(Map<String, dynamic> json) => _$FilmFromJson(json);
  Map<String, dynamic> toJson() => _$FilmToJson(this);
}
